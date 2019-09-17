package it.rentx.backend.repository;

import java.util.List;

import org.springframework.data.repository.CrudRepository;

import it.rentx.backend.models.Annuncio;
import it.rentx.backend.models.Utente;

public interface AnnuncioRepository extends CrudRepository<Annuncio, Long>{

	public List<Annuncio> findByAffittuario(Utente Utente);

	public List<Annuncio> findByPosizione(String posizione);
	
}
