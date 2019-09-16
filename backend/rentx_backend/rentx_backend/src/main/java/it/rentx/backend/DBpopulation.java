package it.rentx.backend;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.ApplicationArguments;
import org.springframework.boot.ApplicationRunner;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Component;

import it.rentx.backend.models.Utente;
import it.rentx.backend.repository.UtenteRepository;
import it.rentx.backend.service.UtenteService;

@Component
public class DBpopulation implements ApplicationRunner {
	@Autowired
	private UtenteService utente;
	@Autowired
	private UtenteRepository utenteRepository;
	@Autowired
	private BCryptPasswordEncoder bCryptPasswordEncoder;

	@Override
	public void run(ApplicationArguments args) throws Exception {
		this.deleteAll();
		this.addAll();
	}
	
	private void deleteAll() {
		utente.deleteAll();
	}

	private void addAll() {
		Utente u1 = new Utente("Gigi", "Finizio", "gigi.finizio@gmail.com", "gigi", "3201234567", "Via Mauritania, Roma", null);
		Utente u2 = new Utente("Mirabella", "Susina", "mira.susi@gmail.com", "albicocca", "3495555000", "Via Torta, Firenze", null);
		Utente u3 = new Utente("Tranquilla", "Liberato", "tl@gmail.com", "password", "3336269898", "Via Santa Chiara, Torino", null);
		Utente u4 = new Utente("Lino", "Baratto", "baratto@gmail.com", "10ottobre", "3478795460", "Via Pozzo Leone, Messina", null);
		
		u1.setPassword(bCryptPasswordEncoder.encode(u1.getPassword()));
        utenteRepository.save(u1);
        u2.setPassword(bCryptPasswordEncoder.encode(u2.getPassword()));
        utenteRepository.save(u2);
        u3.setPassword(bCryptPasswordEncoder.encode(u3.getPassword()));
        utenteRepository.save(u3);
        u4.setPassword(bCryptPasswordEncoder.encode(u4.getPassword()));
        utenteRepository.save(u4);
	}
	
	

}
