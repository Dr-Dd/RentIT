package it.rentx.backend.service;

import java.util.LinkedList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import it.rentx.backend.models.Annuncio;
import it.rentx.backend.repository.AnnuncioRepository;

@Service
public class AnnuncioService {
	
	@Autowired
	AnnuncioRepository repo;
	
	@Transactional
	public Annuncio salvaAnnuncio(Annuncio annuncio) {
		return this.repo.save(annuncio);
	}
	
	@Transactional
	public Annuncio annuncioPerId(Long id){
		return repo.findById(id).get();
	}
	
	@Transactional
	public void deleteAll() {
		this.repo.deleteAll();
	}
	
	@Transactional
	public void delete(Annuncio a) {
		this.repo.delete(a);
	}
	
	@Transactional
	public boolean esiste(Long id) {
		return this.repo.existsById(id);
	}
	
	@Transactional
	public Annuncio aggiornaAnnuncio(Long id, Annuncio a) {
		Annuncio annuncio = this.annuncioPerId(id);
		annuncio.setNomeOggetto(a.getNomeOggetto());
		annuncio.setDescrizione(a.getDescrizione());
		annuncio.setPrezzo(a.getPrezzo());
		
		//solo questi tre perché utente, posizione e data non si possono modificare
		return annuncio;
	}
	
	@Transactional
	public List<Annuncio> annunciBookedPerUtente(long id){
		List<Annuncio> lista=new LinkedList<>();
		for(Annuncio a : this.repo.findByAffittuario_id(id)) {
			if(a.isBooked()) lista.add(a);
		}
		return lista;
	}
	
	@Transactional
	public List<Annuncio> annunciNotBookedPerUtente(long id){
		List<Annuncio> lista=new LinkedList<>();
		for(Annuncio a : this.repo.findByAffittuario_id(id)) {
			if(a.isBooked()==false) lista.add(a);
		}
		return lista;
	}
}

