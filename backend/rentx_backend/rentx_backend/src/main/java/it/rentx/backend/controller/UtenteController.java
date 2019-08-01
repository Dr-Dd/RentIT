package it.rentx.backend.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import it.rentx.backend.models.Risposta;
import it.rentx.backend.models.Utente;
import it.rentx.backend.repository.UtenteRepository;
import it.rentx.backend.service.UtenteService;

@RestController
public class UtenteController {
	
	@Autowired
	UtenteRepository utenteRepository;
	
	@Autowired
	UtenteService utenteService;
	
	@PostMapping("/utente/registrazione")
	public ResponseEntity<Risposta> iscrizioneUtente(@Validated @RequestBody Utente utente) {
		
		if(this.utenteRepository.findByEmail(utente.getEmail()) == null) {
			this.utenteRepository.save(this.utenteService.encodePassword(utente));
			return ResponseEntity.ok().body(new Risposta("Utente iscritto correttamente", "200"));
		} else
			return ResponseEntity.badRequest().body(new Risposta("Errore nell'iscrizione dell'utente", "403"));
	}
	

}
