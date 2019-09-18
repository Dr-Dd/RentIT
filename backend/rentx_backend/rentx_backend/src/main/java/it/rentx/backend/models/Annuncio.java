package it.rentx.backend.models;

import java.util.Date;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.OneToOne;
import javax.persistence.Table;

import org.hibernate.search.annotations.Field;
import org.hibernate.search.annotations.Indexed;
import org.hibernate.search.annotations.TermVector;

@Entity
@Indexed
@Table(name = "annuncio")
public class Annuncio {

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private long id;
	
	@Field(termVector = TermVector.YES)
	private String nomeOggetto;
	
	@Field(termVector = TermVector.YES)
	private String descrizione;
	
	@OneToOne(mappedBy = "immagineAnnuncio", cascade = CascadeType.ALL, fetch = FetchType.LAZY, optional = false)
	private Image immagine;
	
	private float prezzo;
	
	@ManyToOne
	@JoinColumn(name="id_utente")
	private Utente affittuario;
	
	private String posizione;
	
	private Date data;
	
	public Annuncio() {}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public String getNomeOggetto() {
		return nomeOggetto;
	}

	public void setNomeOggetto(String NomeOggetto) {
		nomeOggetto = NomeOggetto;
	}

	public String getDescrizione() {
		return descrizione;
	}

	public void setDescrizione(String Descrizione) {
		descrizione = Descrizione;
	}

	public Image getImmagine() {
		return immagine;
	}

	public void setImmagine(Image Immagine) {
		immagine = Immagine;
	}

	public float getPrezzo() {
		return prezzo;
	}

	public void setPrezzo(float Prezzo) {
		prezzo = Prezzo;
	}

	public Utente getAffittuario() {
		return affittuario;
	}

	public void setAffittuario(Utente Affittuario) {
		affittuario = Affittuario;
	}

	public String getPosizione() {
		return posizione;
	}

	public void setPosizione(String Posizione) {
		posizione = Posizione;
	}

	public Date getData() {
		return data;
	}

	public void setData(Date Data) {
		data = Data;
	}
	
	
	
}
