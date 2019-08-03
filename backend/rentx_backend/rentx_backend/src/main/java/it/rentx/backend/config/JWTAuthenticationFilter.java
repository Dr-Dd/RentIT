package it.rentx.backend.config;

import static com.auth0.jwt.algorithms.Algorithm.HMAC512;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Date;

import javax.servlet.FilterChain;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.AuthenticationException;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;

import com.auth0.jwt.JWT;
import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;

import it.rentx.backend.models.Utente;
import it.rentx.backend.repository.UtenteRepository;

public class JWTAuthenticationFilter extends UsernamePasswordAuthenticationFilter{
	
	private AuthenticationManager authenticationManager;
	
	private UtenteRepository utenteRepository;
	

	public JWTAuthenticationFilter(AuthenticationManager authenticationManager, UtenteRepository utenteRepository) {
        this.authenticationManager = authenticationManager;
        this.utenteRepository = utenteRepository;
    }
	
	@Override
	public Authentication attemptAuthentication(HttpServletRequest req, HttpServletResponse res) {
		
		try {
			Utente credenziali = new ObjectMapper().readValue(req.getInputStream(), Utente.class);
			Utente utente = this.utenteRepository.findByEmail(credenziali.getEmail());
			if(utente != null)
				return authenticationManager.authenticate(new UsernamePasswordAuthenticationToken(credenziali.getEmail(), credenziali.getPassword(), new ArrayList<>()));
			else {
				System.out.println("Utente non esiste");
				return null;
			}
		} catch (IOException e) {
            throw new BadCredentialsException("Credenziali non valide");
		}

	}
	
	@Override
	public void successfulAuthentication(HttpServletRequest req, HttpServletResponse res, FilterChain chain, Authentication auth) throws IOException, ServletException {
		String token = JWT.create()
				.withSubject(((User) auth.getPrincipal()).getUsername())
				.withExpiresAt(new Date(System.currentTimeMillis() + SecurityConstants.EXPIRATION_TIME))
				.sign(HMAC512(SecurityConstants.SECRET.getBytes()));
		
		// Body risposta token
		res.setContentType("application/json");
		PrintWriter out = res.getWriter();
		String email = auth.getName();
		Utente utente = this.utenteRepository.findByEmail(email);
		Long userId = utente.getId();
		out.write("{");
		out.write("\"hasSucceded\": \"true\",\n");
		out.write("\"userId\":" + "\"" + userId + "\",\n");
		out.write("\"Access Token\":" + "\"" + token + "\",\n");
		out.write("\"ResponseMessage\":" + "\"Token creato con successo\"");
		out.write("}");
	}

}
