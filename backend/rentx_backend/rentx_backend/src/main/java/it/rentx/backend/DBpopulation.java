package it.rentx.backend;
/*
import java.util.Date;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.ApplicationArguments;
import org.springframework.boot.ApplicationRunner;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Component;

import it.rentx.backend.models.Annuncio;
import it.rentx.backend.models.Utente;
import it.rentx.backend.repository.UtenteRepository;
import it.rentx.backend.service.AnnuncioService;

@Component

public class DBpopulation implements ApplicationRunner {

	@Autowired
	private UtenteRepository utenteRepository;
	
	@Autowired
	private BCryptPasswordEncoder bCryptPasswordEncoder;
	
	@Autowired
	private AnnuncioService annuncioService;

	@Autowired 
	private ImageService imageService;
	
	@Override
	public void run(ApplicationArguments args) throws Exception {
		this.deleteAll();
		this.addAll();
	}
	
	private void deleteAll() {
		utenteRepository.deleteAll();
	}

	private void addAll() {
		Utente u1 = new Utente("Gigi", "Finizio", "gigi.finizio@gmail.com", "gigi", "3201234567", "Roma", null);
		Utente u2 = new Utente("Mirabella", "Susina", "mira.susi@gmail.com", "albicocca", "3495555000", "Firenze", null);
		Utente u3 = new Utente("Tranquilla", "Liberato", "tl@gmail.com", "password", "3336269898", "Torino", null);
		Utente u4 = new Utente("Lino", "Baratto", "baratto@gmail.com", "10ottobre", "3478795460", "Messina", null);
		
		u1.setPassword(bCryptPasswordEncoder.encode(u1.getPassword()));
        utenteRepository.save(u1);
        u2.setPassword(bCryptPasswordEncoder.encode(u2.getPassword()));
        utenteRepository.save(u2);
        u3.setPassword(bCryptPasswordEncoder.encode(u3.getPassword()));
        utenteRepository.save(u3);
        u4.setPassword(bCryptPasswordEncoder.encode(u4.getPassword()));
        utenteRepository.save(u4);
        
        //nomeOggetto,descrizione, immagine, float prezzo,affittuario, String posizione, Date data
        Annuncio a1 = new Annuncio("Tosaerba", "Tosaerba BOSCHIA potente, alimentato a escermenti di piccione", null, null, 13, u1, u1.getCitta(), new Date());
        Annuncio a2 = new Annuncio("Motosega", "Motosega a benzina, perfetta per le potature", null, null, 8, u1, u1.getCitta(), new Date());
        Annuncio a3 = new Annuncio("Frac", "Noleggio frac, ovvero giacca, pantalone, gilet bianco, papillon bianco e camicia. Nel prezzo è incluso il servizio lavanderia", null, null, 200, u2, u2.getCitta(), new Date());
        Annuncio a4 = new Annuncio("Proiettore", "Videoproiettore con risoluzione altissimissima. Non consuma nemmeno troppo", null, null, 20, u3, u3.getCitta(), new Date());
        Annuncio a5 = new Annuncio("Fotocamera", "Fotocamera professionale super costosa, noleggiabili anche diversi obiettivi", null, null, 40, u3, u3.getCitta(), new Date());
        Annuncio a6 = new Annuncio("Schermo per proiettore", "Schermo 80 Pollici. Supporto Pieghevole", null, null, 10, u3, u3.getCitta(), new Date());
        Annuncio a7 = new Annuncio("Planetaria", "Planetaria per fare i dolci buoni buoni", null, null, 13, u4, u4.getCitta(), new Date());
        Annuncio a8 = new Annuncio("Affettatrice", "Affettatrice elettrica professionale in acciaio inossidabile, spessore max 15 mm, potenza 150 W", null, null, 10, u4, u4.getCitta(), new Date());
        
        annuncioService.salvaAnnuncio(a1);
        annuncioService.salvaAnnuncio(a2);
        annuncioService.salvaAnnuncio(a3);
        annuncioService.salvaAnnuncio(a4);
        annuncioService.salvaAnnuncio(a5);
        annuncioService.salvaAnnuncio(a6);
        annuncioService.salvaAnnuncio(a7);
        annuncioService.salvaAnnuncio(a8);
        
        
	}
	
	

}
*/
