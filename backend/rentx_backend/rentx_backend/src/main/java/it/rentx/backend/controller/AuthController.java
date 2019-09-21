package it.rentx.backend.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import it.rentx.backend.models.Utente;
import it.rentx.backend.models.frontendModel.Richiesta;
import it.rentx.backend.models.frontendModel.Risposta;
import it.rentx.backend.service.AuthService;
import it.rentx.backend.service.UtenteService;

@RestController
@RequestMapping("/auth")
public class AuthController {

	@Autowired
	public UtenteService utenteService;

	@Autowired
	public JavaMailSender emailSender;

	@Autowired
	public AuthService authService;
	private BCryptPasswordEncoder bCryptPasswordEncoder;

	public AuthController(BCryptPasswordEncoder bCryptPasswordEncoder) {
		this.bCryptPasswordEncoder = bCryptPasswordEncoder;
	}

	@DeleteMapping("/logout")
	public ResponseEntity<Risposta> logout(@RequestHeader("Authorization") String token) {
		return ResponseEntity.ok().body(new Risposta("true", "Logout effettuato con successo."));
	}

	@PostMapping("/resetPwd")
	public ResponseEntity<Risposta> resetPassword(@RequestBody Richiesta request) {
		Utente u = this.utenteService.getUtenteByEmail(request.getS());
		System.out.println(u);
		if (this.utenteService.esiste(u.getId())) {
			// Genero la password
			String password_provvisoria = this.authService.randomPassword(10);
			System.out.println(password_provvisoria);
			// Setto l'email provvisoria all'utente
			u.setPassword(bCryptPasswordEncoder.encode(password_provvisoria));
			// Persistenza
			this.utenteService.inserisci(u);

			// Invio per email la password provvisoria
			SimpleMailMessage message = new SimpleMailMessage();
			message.setTo(u.getEmail());
			message.setSubject("Password provvisoria per accedere a RentIT");
			message.setText("La tua password provvisoria è: " + password_provvisoria+ ". Ricorda di cambiarla appena accedi al tuo profilo.");
			emailSender.send(message);
			return ResponseEntity.ok().body(new Risposta("true", "Password provvisoria inviata alla tua email."));
			
		} else
			return ResponseEntity.ok().body(new Risposta("false", "Controlla se hai inserito l'email giusta."));
	}
}
