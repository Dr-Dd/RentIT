package it.rentx.backend.repository;


import java.util.List;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import it.rentx.backend.models.Image;

@Repository
public interface ImageRepository extends CrudRepository<Image, Long>{
	
	public List<Image> findByAnnuncio_id(Long annuncio_id);
}
