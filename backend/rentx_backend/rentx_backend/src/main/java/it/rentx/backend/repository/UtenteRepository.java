package it.rentx.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import it.rentx.backend.models.Utente;

@Repository
public interface UtenteRepository extends JpaRepository<Utente, Long>{

}
