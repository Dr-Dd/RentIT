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

import it.rentx.backend.models.Utente;
import it.rentx.backend.models.frontendModel.ImageModel;
import it.rentx.backend.models.frontendModel.Risposta;
import it.rentx.backend.models.frontendModel.UtenteModel;
import it.rentx.backend.service.UtenteService;

@RestController
@RequestMapping("/utente")
public class UtenteController {
	
	@Autowired
	UtenteService utenteService;
	
    private BCryptPasswordEncoder bCryptPasswordEncoder;

    public UtenteController(BCryptPasswordEncoder bCryptPasswordEncoder) {
        this.bCryptPasswordEncoder = bCryptPasswordEncoder;
    }

    @PostMapping("/registrazione")
    public ResponseEntity<Risposta> registrazioneUtente(@RequestBody Utente utente) {
    	if(utenteService.getUtenteByEmail(utente.getEmail()) != null ) {
    		return ResponseEntity.ok().body(new Risposta("false","Email già registrata"));
    	}
    	utente.setPassword(bCryptPasswordEncoder.encode(utente.getPassword()));
        utenteService.inserisci(utente);
        return ResponseEntity.ok().body(new Risposta("true","Utente iscritto correttamente"));
    }
    
    @GetMapping("/profile")
    public UtenteModel profiloUtente(@RequestHeader("Authorization") String token) {
    	Utente u=this.utenteService.getUtenteByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	return this.utenteService.parseToUtente(u);
    }
    
    @GetMapping("/profile/{id}")
    public UtenteModel profiloUtenteconId(@PathVariable Long id) {
    	//controlla se l'id esiste e in quel caso ritorna l'utente con quell'id (no campi di password ecc)
    	if(this.utenteService.esiste(id)) {
    		Utente u=this.utenteService.trova(id);
    		return this.utenteService.parseToUtente(u);
    	}
    	return null;
    }
    
    // Da terminare
    @PutMapping("/modifica")
    public ResponseEntity<Risposta> modificaUtente(@RequestHeader("Authorization") String token, @RequestBody Utente dati_utente) {
    	Utente utente_da_modificare = this.utenteService.getUtenteByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	utente_da_modificare.setName(dati_utente.getName());
    	utente_da_modificare.setSurname(dati_utente.getSurname());
    	utente_da_modificare.setNumeroTel(dati_utente.getNumeroTel());
    	utente_da_modificare.setCitta(dati_utente.getCitta());
    	
    	// Se la password dell'utente che arriva dalla richiesta è diversa da quello sul db la codifico e la setto altrimenti salvo direttamente
    	if(!(dati_utente.getPassword().equals(utente_da_modificare.getPassword())) && !dati_utente.getPassword().isEmpty())
    		utente_da_modificare.setPassword(bCryptPasswordEncoder.encode(dati_utente.getPassword()));
    	
    	utente_da_modificare.setFirstAccess(false);

    	this.utenteService.inserisci(utente_da_modificare);
    	return ResponseEntity.ok().body(new Risposta("true", "Dati modificati correttamente."));
    }
    
    @DeleteMapping("/elimina")
    public ResponseEntity<Risposta> cancellaUtente(@RequestHeader("Authorization") String token) {
    	
    	//controllare che l'utente non abbia annunci prenotati 
    	//cancellare in caso quelli non prenotati
    	Utente u=this.utenteService.getUtenteByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	if(this.utenteService.haAnnunciPrenotati(u.getId())) {
    		return ResponseEntity.ok().body(new Risposta("false","Non puoi cancellare l'account finche hai annunci prenotati."));
    	}
    	else if (this.utenteService.haAnnunciNonPrenotati(u.getId())) {
    		this.utenteService.eliminaMieiAnnunci(u.getId());
    	}
    	this.utenteService.cancella(u);
    	return ResponseEntity.ok().body(new Risposta("true","Utente eliminato correttamente."));
    }
    
    @PutMapping("/addImage")
    public void aggiungiImmagine(@RequestHeader("Authorization") String token, @RequestBody byte[] image ) {
    	Utente utente = this.utenteService.getUtenteByEmail(this.utenteService.estrazioneEmailDaToken(token));
    	utente.setFotoProfilo(image);
    	
    	this.utenteService.inserisci(utente);
    }
    
    @GetMapping("/image/{id}")
    public ImageModel getImmagineUtente(@RequestHeader("Authorization") String token, @PathVariable Long id) {
    	ImageModel immagine = null;
    	if(id != null) {
    		Utente u = this.utenteService.trova(id);
    		immagine = new ImageModel(u.getFotoProfilo(), u.getId());
    	} 

    	return immagine;
    }

}
