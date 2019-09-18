package it.rentx.backend.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import it.rentx.backend.models.Annuncio;
import it.rentx.backend.models.Utente;
import it.rentx.backend.repository.AnnuncioRepository;

@Service
public class AnnuncioService {
	@Autowired
	AnnuncioRepository repo;
	
	//Manca annunci per affittuario
	
	@Transactional
	public Annuncio salvaAnnuncio(Annuncio annuncio) {
		return repo.save(annuncio);
	}
	
	@Transactional
	public Annuncio annuncioPerId(Long id){
		return repo.findById(id).get();
	}
	
	@Transactional
	public void deleteAll() {
		repo.deleteAll();
	}
	
	@Transactional
	public void delete(Annuncio a) {
		repo.delete(a);
	}
	
	@Transactional
	public Annuncio aggiornaAnnuncio(Long id, Annuncio a) {
		Annuncio annuncio = repo.findById(id).get();
		annuncio.setNomeOggetto(a.getNomeOggetto());
		annuncio.setDescrizione(a.getDescrizione());
		annuncio.setPrezzo(a.getPrezzo());
		//solo questi tre perché utente, posizione e data non si possono modificare
		return annuncio;
	}

	@Transactional
	public List<Annuncio> annunciUtente(Utente affittuario) {
		return repo.findByAffittuario(affittuario);
	}
	
	@Transactional
	public List<Annuncio> annunciUtenteBooked(Utente affittuario, boolean booked) {
		return repo.findByAffittuarioAndBooked(affittuario, booked);
	}
	
	@Transactional
	public List<Annuncio> annunciPosizione(String posizione) {
		return repo.findByPosizione(posizione);
	}
}

