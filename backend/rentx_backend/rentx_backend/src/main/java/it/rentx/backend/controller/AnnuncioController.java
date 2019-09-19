package it.rentx.backend.controller;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import it.rentx.backend.models.Annuncio;
import it.rentx.backend.models.Image;
import it.rentx.backend.models.SearchQuery;
import it.rentx.backend.models.Utente;
import it.rentx.backend.models.frontendModel.AnnuncioModel;
import it.rentx.backend.models.frontendModel.ImageModel;
import it.rentx.backend.models.frontendModel.Risposta;
import it.rentx.backend.service.AnnuncioService;
import it.rentx.backend.service.HibernateSearchService;
import it.rentx.backend.service.ImageService;
import it.rentx.backend.service.UtenteService;

@RestController
@RequestMapping("/annuncio")
public class AnnuncioController {
	
	@Autowired
	private HibernateSearchService searchService;

	@Autowired
	private AnnuncioService annuncioService;
	
	@Autowired
	private UtenteService utenteService;
	
	@Autowired
	private ImageService imageService;
	
	@PostMapping("/newAnnuncio")
    public ResponseEntity<Risposta> nuovoAnnuncio(@RequestHeader("Authorization") String token, @RequestBody Annuncio annuncio) {
		Utente utente = this.utenteService.getUtenteByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	annuncio.setAffittuario(utente);
    	annuncio.setPosizione(utente.getCitta());
		
    	//mi serve l'id dell'annuncio
    	Annuncio a= this.annuncioService.salvaAnnuncio(annuncio);
		
        return ResponseEntity.ok().body(new Risposta("true","Annuncio salvato correttamente",a.getId()));
    }
	
	@DeleteMapping("/elimina/{id}")
    public ResponseEntity<Risposta> cancellaAnnuncio(@RequestHeader("Authorization") String token, @PathVariable("id") Long id) {
    	Annuncio a=this.annuncioService.annuncioPerId(id);
    	if(a.isBooked())
    		return ResponseEntity.ok().body(new Risposta("false", "l'annuncio è ancora prenotato"));
    	else {
    		annuncioService.delete(a);
	    	return ResponseEntity.ok().body(new Risposta("true", "Annuncio eliminato correttamente."));
	    	}
    }
	
	@PutMapping("/modifica/{id}")
	public ResponseEntity<Risposta> modificaAnnuncio(@RequestHeader("Authorization") String token,@RequestBody Annuncio annuncio, @PathVariable("id") Long id) {
    	
		if(this.annuncioService.annuncioPerId(id)!=null) {
			Annuncio adModificato = annuncioService.aggiornaAnnuncio(id, annuncio);
			this.annuncioService.salvaAnnuncio(adModificato);
			return ResponseEntity.ok().body(new Risposta("true", "Dati modificati correttamente."));
		}
		else return ResponseEntity.ok().body(new Risposta("false", "annuncio inesistente")); 
			
    }
	
	
	@PostMapping(value = "/search")
	public List<AnnuncioModel> search(@RequestBody SearchQuery sq) {
		List<Annuncio> searchResults = new ArrayList<>();
		List<AnnuncioModel> annunci = new ArrayList<>();
		try {
			searchResults = this.searchService.fuzzySearch(sq.getOggetto());
		} catch (Exception e) {
			e.printStackTrace();
		}
		for (Annuncio annuncio : searchResults) {
			if((annuncio.getPosizione().equals(sq.getCitta())) &&  !(annuncio.isBooked())) {
				AnnuncioModel tmp = new AnnuncioModel(annuncio.getId(), annuncio.getAffittuario().getId(), annuncio.getAnteprimaImg(), annuncio.getNomeOggetto(), annuncio.getDescrizione(), annuncio.getPrezzo(), annuncio.getPosizione(), annuncio.getData());
				annunci.add(tmp);
			}
		}
		/*Ovviamente questa è una cafonata, mi serviva solo per provare velocemente
		 * il metodo. Probabilmente va implementata passando 
		 * 	annuncioService.annunciPosizione(sq.getCitta());
		 * come insieme su cui fare la ricerca fuzzy
		 */
		return annunci;
	}
	
	// Da Modificare
	@GetMapping("/annunci/{id}")
	public List<AnnuncioModel> AnnunciUtente(@RequestHeader("Authorization") String token, @PathVariable("id") Long id) {
		if(token!=null) {	
			if(id == this.utenteService.getUtenteByEmail(this.utenteService.estrazioneEmailDaToken(token)).getId()) {
					return this.annuncioService.parseToAnnuncioList(this.annuncioService.annunciBookedPerUtente(id));
				}
				else return Collections.emptyList();
			} else
				return this.annuncioService.parseToAnnuncioList(this.annuncioService.annunciNotBookedPerUtente(id));
	}
	
	@GetMapping("/annuncio/{id}")
	public AnnuncioModel SingoloAnnuncio(@PathVariable("id") Long id) {
		
		AnnuncioModel am=this.annuncioService.parseToAnnuncio(this.annuncioService.annuncioPerId(id));
		
		return am;
	}
	
	@PutMapping("/addImage/{id}")
	public void aggiungiImmagine(@RequestHeader("Authorization") String token, @PathVariable("id") Long id,@RequestBody byte[] image) {
		Annuncio a= this.annuncioService.annuncioPerId(id);
		if(a.getAffittuario().getId()==this.utenteService.getUtenteByEmail(this.utenteService.estrazioneEmailDaToken(token)).getId()) {
			Image i=new Image();
			i.setAnnuncio(a);
			i.setData(image);
			this.imageService.inserisci(i);
		}
	}
	
	@GetMapping("/images/{id}")
	public List<ImageModel> getAnnuncioImages(@PathVariable("id") Long id){
		if(this.annuncioService.esiste(id)) {
			return this.imageService.parseToImageList(this.imageService.getImagePerIdAnn(id));
		}
		else return Collections.emptyList();
	}
	
	@PutMapping("/prenota/{id}")
	public void prenotaAnnuncio(@RequestHeader("Authorization") String token, @PathVariable("id") Long id) {
		Annuncio a= this.annuncioService.annuncioPerId(id);
		if(a.getAffittuario().getId()==this.utenteService.getUtenteByEmail(this.utenteService.estrazioneEmailDaToken(token)).getId()) {
			a.setBooked(true);
			this.annuncioService.salvaAnnuncio(a);
		}
	}	
	
	@PutMapping("/libera/{id}")
	public void liberaAnnuncio(@RequestHeader("Authorization") String token, @PathVariable("id") Long id) {
		Annuncio a= this.annuncioService.annuncioPerId(id);
		if(a.getAffittuario().getId()==this.utenteService.getUtenteByEmail(this.utenteService.estrazioneEmailDaToken(token)).getId()) {
			a.setBooked(false);
			this.annuncioService.salvaAnnuncio(a);
		}
	}	

}
