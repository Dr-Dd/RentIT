package it.rentx.backend.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.auth0.jwt.JWT;
import com.auth0.jwt.algorithms.Algorithm;

import it.rentx.backend.config.SecurityConstants;
import it.rentx.backend.repository.UtenteRepository;

@Service
public class UtenteService {
	
	@Autowired
	UtenteRepository repo;
	
	public String estrazioneEmailDaToken(String token) {
		return JWT.require(Algorithm.HMAC512(SecurityConstants.SECRET.getBytes()))
				.build()
				.verify(token.replace(SecurityConstants.TOKEN_PREFIX, "")) //Rimuovo bearer dal token
				.getSubject();
	}
	
	@Transactional
	public void deleteAll() {
		repo.deleteAll();
	}
}
