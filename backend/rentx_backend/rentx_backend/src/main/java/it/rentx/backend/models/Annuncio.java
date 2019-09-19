package it.rentx.backend.models;

import java.util.Date;
import java.util.List;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Lob;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;
import javax.persistence.Table;

import org.hibernate.search.annotations.Field;
import org.hibernate.search.annotations.Indexed;
import org.hibernate.search.annotations.TermVector;

@Entity
@Indexed
@Table(name = "annuncio")
public class Annuncio {

	@Id
	@GeneratedValue(strategy = GenerationType.SEQUENCE)
	private long id;
	
	@ManyToOne
	private Utente affittuario;
	
	@Lob
	@Column(name = "immagine_copertina", length = 100000)
	private byte[] anteprimaImg;
	
	@Field(termVector = TermVector.YES)
	private String nomeOggetto;
	
	@Field(termVector = TermVector.YES)
	private String descrizione;
	
	private float prezzo;
	
	private String posizione;
	
	private Date data;
	
	private boolean booked = false;
	
	@OneToMany(mappedBy = "annuncio")
	private List<Image> immagini;
	
	public Annuncio() {}

	public Annuncio(String nomeOggetto, String descrizione, byte[] anteprimaImg, float prezzo, Utente affittuario, String posizione, Date data, List<Image> immagini) {
		super();
		this.nomeOggetto = nomeOggetto;
		this.descrizione = descrizione;
		this.anteprimaImg = anteprimaImg;
		this.prezzo = prezzo;
		this.affittuario = affittuario;
		this.posizione = posizione;
		this.data = data;
		this.immagini = immagini;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public Utente getAffittuario() {
		return affittuario;
	}

	public void setAffittuario(Utente affittuario) {
		this.affittuario = affittuario;
	}

	public byte[] getAnteprimaImg() {
		return anteprimaImg;
	}

	public void setAnteprimaImg(byte[] anteprimaImg) {
		this.anteprimaImg = anteprimaImg;
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

	public float getPrezzo() {
		return prezzo;
	}

	public void setPrezzo(float prezzo) {
		this.prezzo = prezzo;
	}

	public String getPosizione() {
		return posizione;
	}

	public void setPosizione(String posizione) {
		this.posizione = posizione;
	}

	public Date getData() {
		return data;
	}

	public void setData(Date data) {
		this.data = data;
	}

	public boolean isBooked() {
		return booked;
	}

	public void setBooked(boolean booked) {
		this.booked = booked;
	}

	public List<Image> getImmagini() {
		return immagini;
	}

	public void setImmagini(List<Image> immagini) {
		this.immagini = immagini;
	}
	
}
