package it.rentx.backend.service;

import static java.util.Collections.emptyList;

import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Component;

import it.rentx.backend.models.Utente;
import it.rentx.backend.repository.UtenteRepository;

@Component
public class UserDetailsServiceImpl implements UserDetailsService {
	
	private UtenteRepository utenteRepository;

	public UserDetailsServiceImpl(UtenteRepository utenteRepository) {
		this.utenteRepository = utenteRepository;
	}

	@Override
	public UserDetails loadUserByUsername(String email) throws UsernameNotFoundException {
		Utente utente = utenteRepository.findByEmail(email);
		if (utente == null) {
			throw new UsernameNotFoundException(email);
		}
		return new User(utente.getEmail(), utente.getPassword(), emptyList());
	}
}
