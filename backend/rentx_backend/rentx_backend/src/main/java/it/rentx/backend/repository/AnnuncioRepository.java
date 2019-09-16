package it.rentx.backend.repository;

import org.springframework.data.repository.CrudRepository;

import it.rentx.backend.models.Annuncio;

public interface AnnuncioRepository extends CrudRepository<Annuncio, Long>{

}
