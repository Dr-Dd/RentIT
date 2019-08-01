package it.rentx.backend.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

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
	public ResponseEntity<String> iscrizioneUtente(@Validated @RequestBody Utente utente) {
		this.utenteRepository.save(this.utenteService.encodePassword(utente));
		return ResponseEntity.ok("Dai Cane");
	}
}
