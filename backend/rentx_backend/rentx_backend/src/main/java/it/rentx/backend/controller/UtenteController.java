package it.rentx.backend.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
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
    	if(utenteRepository.findByEmail(utente.getEmail()) != null ) {
    		return ResponseEntity.ok().body(new Risposta("false","","Email già registrata", null));
    	}
    	utente.setPassword(bCryptPasswordEncoder.encode(utente.getPassword()));
        utenteRepository.save(utente);
        return ResponseEntity.ok().body(new Risposta("true","","Utente iscritto correttamente", null));
    }
    
    @GetMapping("/profile")
    public Utente profiloUtente(@RequestHeader("Authorization") String token) {
    	return this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token));
    }
    
    // Da terminare
    @PutMapping("/modifica")
    public ResponseEntity<Risposta> modificaUtente(@RequestHeader("Authorization") String token, @RequestBody Utente utente_modificato) {
    	Utente utente_da_modificare = this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	//utente_da_modificare.setNumero(dati_utente.getNumero());
    	//utente_da_modificare.setAddress(dati_utente.getAddress());
    	//String password = utente_da_modificare.getPassword();
    	
    	// Se la password dell'utente che arriva dalla richiesta è diversa da quello sul db la codifico e la setto altrimenti salvo direttamente
    	if(!utente_modificato.getPassword().equals(utente_da_modificare.getPassword()))
    		utente_modificato.setPassword(bCryptPasswordEncoder.encode(utente_modificato.getPassword()));
    	
    	
    	this.utenteRepository.delete(utente_da_modificare);

    	this.utenteRepository.save(utente_modificato);
    	return ResponseEntity.ok().body(new Risposta("true", utente_modificato.getId(), "", "Dati modificati correttamente."));
    }
    
    @DeleteMapping("/elimina")
    public ResponseEntity<Risposta> cancellaUtente(@RequestHeader("Authorization") String token) {
    	this.utenteRepository.delete(this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token)));
    	return ResponseEntity.ok().body(new Risposta("true", "", "Utente eliminato correttamente.", null));
    }
	

}
