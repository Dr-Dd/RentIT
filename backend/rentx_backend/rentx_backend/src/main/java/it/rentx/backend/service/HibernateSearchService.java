package it.rentx.backend.service;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.NoResultException;
import javax.transaction.Transactional;

import org.apache.lucene.search.Query;
import org.hibernate.search.jpa.FullTextEntityManager;
import org.hibernate.search.jpa.Search;
import org.hibernate.search.query.dsl.QueryBuilder;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import it.rentx.backend.models.Annuncio;

@Service
public class HibernateSearchService {
	
	private final EntityManager centityManager;
	
	@Autowired
	public HibernateSearchService(final EntityManagerFactory entityManager) {
		super();
		this.centityManager = entityManager.createEntityManager();
	}
	
	public void initializeHibernateSearch() {
		try {
			FullTextEntityManager fullTextEntityManager = Search.getFullTextEntityManager(centityManager);
			fullTextEntityManager.createIndexer().startAndWait();
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}
	
	@Transactional
	public List<Annuncio> fuzzySearch(String searchTerm) {
		FullTextEntityManager fullTextEntityManager = Search.getFullTextEntityManager(centityManager);
		QueryBuilder qb = fullTextEntityManager.getSearchFactory().buildQueryBuilder().forEntity(Annuncio.class).get();
		Query luceneQuery = qb.keyword().fuzzy().withEditDistanceUpTo(1).withPrefixLength(1).onFields("<INSERT-FIELD-HERE>").matching(searchTerm).createQuery();
		
		javax.persistence.Query jpaQuery = fullTextEntityManager.createFullTextQuery(luceneQuery, Annuncio.class);
		
		// esegue la ricerca ^^
		
		List<Annuncio> listaAnnuncio = null;
		try {
			listaAnnuncio =  jpaQuery.getResultList();
		} catch (NoResultException nre) {
			// stoppa l'esecuzione (non fa nulla)
		}
		return listaAnnuncio;
	}
}
