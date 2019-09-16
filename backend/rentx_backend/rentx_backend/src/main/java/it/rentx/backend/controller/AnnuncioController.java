package it.rentx.backend.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;

import it.rentx.backend.models.Annuncio;
import it.rentx.backend.models.Risposta;
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

	@RequestMapping(value = "/search", method = RequestMethod.GET)
	public List<Annuncio> search(@RequestParam(value = "search", required = false) String q) {
		List<Annuncio> searchResults = null;
		try {
			searchResults = searchService.fuzzySearch(q);
		} catch (Exception e) {
			// decidere cosa (e se) gestire eccezioni
		}
		return searchResults;
	}
	
	@PostMapping("/newAnnuncio")
    public ResponseEntity<Risposta> nuovoAnnuncio(@RequestHeader("Authorization") String token, @RequestBody Annuncio annuncio) {
		Utente utente = this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	annuncio.setAffittuario(utente);
    	annuncio.setPosizione(utente.getAddress());
		annuncioService.salvaAnnuncio(annuncio);
        return ResponseEntity.ok().body(new Risposta("true","","Annuncio salvato correttamente", null));
    }
	
	@PutMapping("/modifica/{id}")
	public ResponseEntity<Risposta> modificaAnnuncio(@RequestHeader("Authorization") String token,
			@RequestBody Annuncio annuncio, @PathVariable("id") Long id) {
    	
		Annuncio adModificato = annuncioService.aggiornaAnnuncio(id, annuncio);
		
    	return ResponseEntity.ok().body(new Risposta("true", adModificato.getId(), "", "Dati modificati correttamente."));
    }
	
	@DeleteMapping("/elimina/{id}")
    public ResponseEntity<Risposta> cancellaAnnuncio(@RequestHeader("Authorization") String token, @PathVariable("id") Long id) {
    	annuncioService.delete(annuncioService.annuncioPerId(id));
    	return ResponseEntity.ok().body(new Risposta("true", "", "Annuncio eliminato correttamente.", null));
    }

}
