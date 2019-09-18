package it.rentx.backend.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import it.rentx.backend.models.ImageModel;
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
    		return ResponseEntity.ok().body(new Risposta("false","Email già registrata"));
    	}
    	utente.setPassword(bCryptPasswordEncoder.encode(utente.getPassword()));
        utenteRepository.save(utente);
        return ResponseEntity.ok().body(new Risposta("true","Utente iscritto correttamente"));
    }
    
    @GetMapping("/profile")
    public Utente profiloUtente(@RequestHeader("Authorization") String token) {
    	return this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token));
    }
    
    // Da terminare
    @PutMapping("/modifica")
    public ResponseEntity<Risposta> modificaUtente(@RequestHeader("Authorization") String token, @RequestBody Utente dati_utente) {
    	Utente utente_da_modificare = this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	utente_da_modificare.setName(dati_utente.getName());
    	utente_da_modificare.setSurname(dati_utente.getSurname());
    	utente_da_modificare.setNumero(dati_utente.getNumero());
    	utente_da_modificare.setAddress(dati_utente.getAddress());
    	
    	// Se la password dell'utente che arriva dalla richiesta è diversa da quello sul db la codifico e la setto altrimenti salvo direttamente
    	if(!dati_utente.getPassword().equals(utente_da_modificare.getPassword()))
    		utente_da_modificare.setPassword(bCryptPasswordEncoder.encode(dati_utente.getPassword()));
    	
    	utente_da_modificare.setFirstAccess(false);

    	this.utenteRepository.save(utente_da_modificare);
    	return ResponseEntity.ok().body(new Risposta("true", "Dati modificati correttamente."));
    }
    
    @DeleteMapping("/elimina")
    public ResponseEntity<Risposta> cancellaUtente(@RequestHeader("Authorization") String token) {
    	this.utenteRepository.delete(this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token)));
    	return ResponseEntity.ok().body(new Risposta("true","Utente eliminato correttamente."));
    }
    
    @PutMapping("/addImage")
    public void aggiungiImmagine(@RequestHeader("Authorization") String token, @RequestBody byte[] image ) {
    	Utente utente = this.utenteRepository.findByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	utente.setImage(image);
    	this.utenteRepository.save(utente); 	
    }
    
    @GetMapping("/image/{id}")
    public ImageModel getImmagineUtente(@RequestHeader("Authorization") String token, @PathVariable Long id) {
    	ImageModel immagine = null;
    	if(id != null) {
    		Utente u = this.utenteRepository.findById(id).get();
    		immagine = new ImageModel(u.getImage(), u.getId());
    	} 

    	return immagine;
    }

}
