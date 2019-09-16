package it.rentx.backend.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;

import it.rentx.backend.models.Annuncio;
import it.rentx.backend.repository.AnnuncioRepository;
import it.rentx.backend.service.AnnuncioService;
import it.rentx.backend.service.HibernateSearchService;

@Controller
@RequestMapping("/annuncio")
public class AnnuncioController {
	
	@Autowired
	private HibernateSearchService searchService;

	@Autowired
	private AnnuncioService annuncioService;
	
	@Autowired 
	private AnnuncioRepository annuncioRepository;

	@RequestMapping(value = "/search", method = RequestMethod.GET)
	public List<Annuncio> search(@RequestParam(value = "search", required = false) String q) {
		List<Annuncio> searchResults = null;
		try {
			searchResults = searchService.fuzzySearch(q);
		} catch (Exception e) {
			// decidere cosa (e se) gestire eccezioni
		}
		return searchResults;
	}

}
