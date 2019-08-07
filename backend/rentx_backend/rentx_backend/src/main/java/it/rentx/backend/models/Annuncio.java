package it.rentx.backend.models;

import java.time.LocalDate;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToOne;
import javax.persistence.OneToOne;
import javax.persistence.Table;

import org.javamoney.moneta.Money;

@Entity
@Table(name = "annuncio")
public class Annuncio {

	@Id
	@GeneratedValue(strategy = GenerationType.SEQUENCE)
	private long id;

	@Column(name = "nomeoggetto_annuncio")
	private String nomeOggetto;

	@Column(name = "descrizione_annuncio")
	private String descrizione;

	@Column(name = "prezzo_annuncio")
	private Money prezzo;

	@Column(name = "datainserimento_annuncio")
	private LocalDate dataInserimento;

	@ManyToOne
	private Utente utenteLocatore;

	@OneToOne(mappedBy = "annuncio", cascade = CascadeType.ALL, fetch = FetchType.LAZY, optional = false)
	private Image image;

	public Annuncio() {}

	public Annuncio(String nomeOggetto, String descrizione, Money prezzo, LocalDate dataInserimento, Image image) {
		this.nomeOggetto = nomeOggetto;
		this.descrizione = descrizione;
		this.prezzo = prezzo;
		this.dataInserimento = dataInserimento;
		this.image = image;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public String getNomeOggetto() {
		return nomeOggetto;
	}

	public void setNomeOggetto(String nomeOggetto) {
		this.nomeOggetto = nomeOggetto;
	}

	public String getDescrizione() {
		return descrizione;
	}

	public void setDescrizione(String descrizione) {
		this.descrizione = descrizione;
	}

	public Money getPrezzo() {
		return prezzo;
	}

	public void setPrezzo(Money prezzo) {
		this.prezzo = prezzo;
	}

	public LocalDate getDataInserimento() {
		return dataInserimento;
	}

	public void setDataInserimento(LocalDate dataInserimento) {
		this.dataInserimento = dataInserimento;
	}

	public Utente getUtenteLocatore() {
		return utenteLocatore;
	}

	public void setUtenteLocatore(Utente utenteLocatore) {
		this.utenteLocatore = utenteLocatore;
	}

	public Image getImage() {
		return image;
	}

	public void setImage(Image image) {
		this.image = image;
	}

	
}
