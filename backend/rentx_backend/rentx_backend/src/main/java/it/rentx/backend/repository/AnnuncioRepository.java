package it.rentx.backend.repository;

import java.util.List;

import org.springframework.data.repository.CrudRepository;

import it.rentx.backend.models.Annuncio;

public interface AnnuncioRepository extends CrudRepository<Annuncio, Long>{
	
	public List<Annuncio> findByAffittuario_id(Long affituario_id);
	
	public void deleteAllByAffittuario_id(Long affittuario_id);
	
}
