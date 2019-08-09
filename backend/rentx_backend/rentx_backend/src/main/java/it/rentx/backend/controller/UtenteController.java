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
        utente.setPassword(bCryptPasswordEncoder.encode(utente.getPassword()));
        utenteRepository.save(utente);
        return ResponseEntity.ok().body(new Risposta("true","","Utente iscritto correttamente", null));
    }
    
    @GetMapping("/profile")
    public ResponseEntity<Risposta> profiloUtente(@RequestHeader("Authorization") String token) {
    	Utente u = this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	if(u != null) {
	    	Utente utente_modificato = new Utente();
	    	utente_modificato.setId(u.getId());
	    	utente_modificato.setName(u.getName());
	    	utente_modificato.setSurname(u.getSurname());
	    	utente_modificato.setEmail(u.getEmail());
	    	utente_modificato.setNumero(u.getNumero());
	    	utente_modificato.setAddress(u.getAddress());
	    	utente_modificato.setImage(u.getImage());
	    	
	    	System.out.println(utente_modificato.getId());
	    	return ResponseEntity.ok().body(new Risposta("true", "", "Utente trovato", utente_modificato));
    	} else 
    		return ResponseEntity.badRequest().body(new Risposta("false", "", "Utente non trovato", null));
    }
    
    // Da terminare
    // Nel body ricevo tutto tranne l'immagine e se un campo non viene cambiato allora stringa vuota. Se c'è una stringa vuota non ci devono essere cambiamenti
    @PutMapping("/modifica")
    public ResponseEntity<Risposta> modificaUtente(@RequestHeader("Authorization") String token, @RequestBody Utente dati_utente) {
    	Utente utente_da_modificare = this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	utente_da_modificare.setNumero(dati_utente.getNumero());
    	utente_da_modificare.setAddress(dati_utente.getAddress());
    	
    	this.utenteRepository.save(utente_da_modificare);
    	
    	return ResponseEntity.ok().body(new Risposta("true", utente_da_modificare.getId(), "", "Dati modificati correttamente."));
    }
    
    
    // Devo cambiare il messaggio di risposta con "Il tuo account verrà cancellato solo quando tutti i prestiti sarannno terminati"
    // Effettuare il controllo se i prestiti sono terminati, se vero cancello altimenti messaggio errore
    @DeleteMapping("/elimina")
    public ResponseEntity<Risposta> cancellaUtente(@RequestHeader("Authorization") String token) {
    	this.utenteRepository.delete(this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token)));
    	return ResponseEntity.ok().body(new Risposta("true", "", "Utente eliminato correttamente.", null));
    }
	

}
