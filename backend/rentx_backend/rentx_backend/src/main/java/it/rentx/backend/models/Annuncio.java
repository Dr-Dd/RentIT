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

@Entity
@Table(name = "annuncio")
public class Annuncio {

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private long id;
	
	private String NomeOggetto;
	
	private String Descrizione;
	
	@OneToOne(mappedBy = "immagineAnnuncio", cascade = CascadeType.ALL, fetch = FetchType.LAZY, optional = false)
	private Image Immagine;
	
	private float Prezzo;
	
	@ManyToOne
	@JoinColumn(name="id_utente")
	private Utente Affittuario;
	
	private String Posizione;
	
	private Date Data;
	
	public Annuncio() {}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public String getNomeOggetto() {
		return NomeOggetto;
	}

	public void setNomeOggetto(String nomeOggetto) {
		NomeOggetto = nomeOggetto;
	}

	public String getDescrizione() {
		return Descrizione;
	}

	public void setDescrizione(String descrizione) {
		Descrizione = descrizione;
	}

	public Image getImmagine() {
		return Immagine;
	}

	public void setImmagine(Image immagine) {
		Immagine = immagine;
	}

	public float getPrezzo() {
		return Prezzo;
	}

	public void setPrezzo(float prezzo) {
		Prezzo = prezzo;
	}

	public Utente getAffittuario() {
		return Affittuario;
	}

	public void setAffittuario(Utente affittuario) {
		Affittuario = affittuario;
	}

	public String getPosizione() {
		return Posizione;
	}

	public void setPosizione(String posizione) {
		Posizione = posizione;
	}

	public Date getData() {
		return Data;
	}

	public void setData(Date data) {
		Data = data;
	}
	
	
	
}
