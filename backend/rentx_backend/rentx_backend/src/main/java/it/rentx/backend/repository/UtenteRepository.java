package it.rentx.backend.repository;

import javax.transaction.Transactional;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import it.rentx.backend.models.Utente;

@Repository
public interface UtenteRepository extends CrudRepository<Utente, Long>{
	
	@Transactional
	public Utente findByEmail(String email);
}
