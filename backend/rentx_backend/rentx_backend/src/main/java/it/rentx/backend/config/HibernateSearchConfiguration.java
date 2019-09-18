package it.rentx.backend.config;

import javax.persistence.EntityManagerFactory;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import it.rentx.backend.service.HibernateSearchService;

@EnableAutoConfiguration
@Configuration
public class HibernateSearchConfiguration {
	
	@Autowired
	private EntityManagerFactory bentityManager;
	
	@Bean
	HibernateSearchService hibernateSearchService() {
		HibernateSearchService hibernateSearchService = new HibernateSearchService(bentityManager);
		hibernateSearchService.initializeHibernateSearch();
		return hibernateSearchService;
	}

}