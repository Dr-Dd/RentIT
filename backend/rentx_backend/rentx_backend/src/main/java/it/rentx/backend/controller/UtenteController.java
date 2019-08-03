package it.rentx.backend.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import it.rentx.backend.models.Risposta;
import it.rentx.backend.models.Utente;
import it.rentx.backend.repository.UtenteRepository;
import it.rentx.backend.service.UtenteService;

@RestController
@RequestMapping("/utente")
public class UtenteController {
	
	@Autowired
	UtenteService utenteService;
	
	private UtenteRepository utenteRepository;
    private BCryptPasswordEncoder bCryptPasswordEncoder;

    public UtenteController(UtenteRepository utenteRepository,BCryptPasswordEncoder bCryptPasswordEncoder) {
        this.utenteRepository = utenteRepository;
        this.bCryptPasswordEncoder = bCryptPasswordEncoder;
    }

    @PostMapping("/registrazione")
    public ResponseEntity<Risposta> registrazioneUtente(@RequestBody Utente utente) {
        utente.setPassword(bCryptPasswordEncoder.encode(utente.getPassword()));
        utenteRepository.save(utente);
        return ResponseEntity.ok().body(new Risposta("true","","Utente iscritto correttamente"));
    }
    
    @GetMapping("/profile")
    public Utente profiloUtente(@RequestHeader("Authorization") String token) {
    	String email_utente = this.utenteService.estrazioneEmailDaToken(token);
    	Utente u = this.utenteRepository.findByEmail(email_utente);
    	Utente u_risultato = new Utente(u.getId(), u.getName(), u.getSurname(), u.getEmail(), u.getNumero(), u.getAddress());
    	return u_risultato;
    }
	

}
