package it.rentx.backend.repository;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import it.rentx.backend.models.Utente;

@Repository
public interface UtenteRepository extends CrudRepository<Utente, Long>{
	
	public Utente findByEmail(String email);
}
