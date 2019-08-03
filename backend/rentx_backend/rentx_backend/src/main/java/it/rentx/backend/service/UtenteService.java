package it.rentx.backend.service;

import org.springframework.stereotype.Service;

import com.auth0.jwt.JWT;
import com.auth0.jwt.algorithms.Algorithm;

import it.rentx.backend.config.SecurityConstants;

@Service
public class UtenteService {
	
	
	public String estrazioneEmailDaToken(String token) {
		return JWT.require(Algorithm.HMAC512(SecurityConstants.SECRET.getBytes()))
				.build()
				.verify(token.replace(SecurityConstants.TOKEN_PREFIX, "")) //Rimuovo bearer dal token
				.getSubject();
	}
}
