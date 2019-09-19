package it.rentx.backend.service;

import javax.transaction.Transactional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.auth0.jwt.JWT;
import com.auth0.jwt.algorithms.Algorithm;

import it.rentx.backend.config.SecurityConstants;
import it.rentx.backend.models.Utente;
import it.rentx.backend.models.frontendModel.UtenteModel;
import it.rentx.backend.repository.UtenteRepository;

@Service
public class UtenteService {
	
	 @Autowired
	 private UtenteRepository utenteRepo;
	 
	 @Autowired
	 private AnnuncioService annuncioService;
	
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
	
	 public UtenteModel parseToUtente(Utente u) {
		 UtenteModel um=new UtenteModel(u.getId(),u.getName(), u.getSurname(), u.getEmail(), "", u.getNumeroTel(), u.getCitta());
		 return um;
	 }
	 
	 @Transactional
	 public boolean haAnnunciPrenotati(Long idU) {
		 if(this.annuncioService.annunciBookedPerUtente(idU).isEmpty())
			 return false;
		 return true;
	 }
	 
	 @Transactional
	 public boolean haAnnunciNonPrenotati(Long idU) {
		 if(this.annuncioService.annunciNotBookedPerUtente(idU).isEmpty())
			 return false;
		 return true;
	 }
	
	 @Transactional
	 public void eliminaMieiAnnunci(Long id) {
		 this.annuncioService.cancellaTuttiPerId(id);
	 }
	
}
