package it.rentx.backend.service;

import javax.transaction.Transactional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.auth0.jwt.JWT;
import com.auth0.jwt.algorithms.Algorithm;

import it.rentx.backend.config.SecurityConstants;
import it.rentx.backend.models.Utente;
import it.rentx.backend.repository.UtenteRepository;

@Service
public class UtenteService {
	
	 @Autowired
	 private UtenteRepository utenteRepo;
	
	public String estrazioneEmailDaToken(String token) {
		return JWT.require(Algorithm.HMAC512(SecurityConstants.SECRET.getBytes()))
				.build()
				.verify(token.replace(SecurityConstants.TOKEN_PREFIX, "")) //Rimuovo bearer dal token
				.getSubject();
	}
	
	 @Transactional
	 public Utente getUtenteByEmail(String email) {
		 return this.utenteRepo.findByEmail(email);
	 }
	 
	 @Transactional
	 public Utente inserisci(Utente u) {
		 return this.utenteRepo.save(u);
	 }
	 
	 @Transactional
	 public void cancella(Utente u) {
		 this.utenteRepo.delete(u);
	 }
	 
	 @Transactional
	 public Utente trova(Long id) {
		 return this.utenteRepo.findById(id).get();
	 }
	 
	 @Transactional
	 public boolean esiste(Long id) {
		 return this.utenteRepo.existsById(id);
	 }
	
	
	
}
