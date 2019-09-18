package it.rentx.backend.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;

import it.rentx.backend.models.Annuncio;
import it.rentx.backend.models.Risposta;
import it.rentx.backend.models.SearchQuery;
import it.rentx.backend.models.Utente;
import it.rentx.backend.repository.UtenteRepository;
import it.rentx.backend.service.AnnuncioService;
import it.rentx.backend.service.HibernateSearchService;
import it.rentx.backend.service.UtenteService;

@Controller
@RequestMapping("/annuncio")
public class AnnuncioController {
	
	@Autowired
	private HibernateSearchService searchService;

	@Autowired
	private AnnuncioService annuncioService;
	
	@Autowired
	private UtenteService utenteService;
	
	@Autowired
	private UtenteRepository utenteRepository;

	@PostMapping(value = "/search")
	public List<Annuncio> search(@RequestBody SearchQuery sq) {
		List<Annuncio> searchResults = null;
		try {
			searchResults = searchService.fuzzySearch(sq.getOggetto());
		} catch (Exception e) {
			// decidere cosa (e se) gestire eccezioni
		}
		for (Annuncio annuncio : searchResults) {
			if(annuncio.getPosizione() != sq.getCitta())
				searchResults.remove(annuncio);
		}
		/*Ovviamente questa è una cafonata, mi serviva solo per provare velocemente
		 * il metodo. Probabilmente va implementata passando 
		 * 	annuncioService.annunciPosizione(sq.getCitta());
		 * come insieme su cui fare la ricerca fuzzy
		 */
		return searchResults;
	}
	
	@PostMapping("/newAnnuncio")
    public ResponseEntity<Risposta> nuovoAnnuncio(@RequestHeader("Authorization") String token, @RequestBody Annuncio annuncio) {
		Utente utente = this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	annuncio.setAffittuario(utente);
    	annuncio.setPosizione(utente.getAddress());
		annuncioService.salvaAnnuncio(annuncio);
        return ResponseEntity.ok().body(new Risposta("true","Annuncio salvato correttamente"));
    }
	
	@PutMapping("/modifica/{id}")
	public ResponseEntity<Risposta> modificaAnnuncio(@RequestHeader("Authorization") String token,@RequestBody Annuncio annuncio, @PathVariable("id") Long id) {
    	
		Annuncio adModificato = annuncioService.aggiornaAnnuncio(id, annuncio);
		this.annuncioService.salvaAnnuncio(adModificato);
		
    	return ResponseEntity.ok().body(new Risposta("true", "Dati modificati correttamente."));
    }
	
	@DeleteMapping("/elimina/{id}")
    public ResponseEntity<Risposta> cancellaAnnuncio(@RequestHeader("Authorization") String token, @PathVariable("id") Long id) {
    	annuncioService.delete(annuncioService.annuncioPerId(id));
    	return ResponseEntity.ok().body(new Risposta("true", "Annuncio eliminato correttamente."));
    }
	
	// Devo sistemare dato che devo prendere gli annunci dell'utente a cui corrisponde questo id (controlla i model)
	@GetMapping("/annunci/{id}")
	public List<Annuncio> AnnunciUtente(@RequestHeader("Authorization") String token, @PathVariable("id") Long id) {
		if(!token.isEmpty()) {
			Utente attuale = this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token));
			if (attuale.getId() == id) {
				return annuncioService.annunciUtenteBooked(attuale, true);
			}
		}
		
    	return annuncioService.annunciUtente(utenteRepository.findById(id).get());
	}

}
