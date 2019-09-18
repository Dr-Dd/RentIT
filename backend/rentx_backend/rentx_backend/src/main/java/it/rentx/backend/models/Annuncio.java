package it.rentx.backend.models;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.Lob;
import javax.persistence.ManyToOne;
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
	
	@Lob
	@Column(name = "immagine_annuncio")
	private byte[] anteprimaImg;
	
	@Column
	private float prezzo;
	
	@ManyToOne
	@JoinColumn(name="id_utente")
	private Utente affittuario;
	
	@Column
	private String posizione;
	
	@Column
	private Date data;
	
	@Column
	private boolean booked;
	
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

	public byte[] getAnteprimaImg() {
		return anteprimaImg;
	}

	public void setAnteprimaImg(byte[] anteprimaImg) {
		this.anteprimaImg = anteprimaImg;
	}

	public boolean isBooked() {
		return booked;
	}

	public void setBooked(boolean booked) {
		this.booked = booked;
	}
	
	
	
}
