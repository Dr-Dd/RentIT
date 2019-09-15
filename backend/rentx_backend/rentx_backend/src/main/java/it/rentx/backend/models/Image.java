package it.rentx.backend.models;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.OneToOne;
import javax.persistence.Table;

@Entity
@Table(name = "immagine")
public class Image {

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private long id;
	
	@OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "id_utente")
	private Utente utente;
	
	@OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "id_annuncio")
	private Annuncio immagineAnnuncio;
	
	@Column(name = "immagine")
	private Byte[] image;
	
	public Image() {}
	
	public Image(Long id, Utente utente) {
		this.id = id;
		this.utente = utente;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
	}

	public Utente getUtente() {
		return utente;
	}

	public void setUtente(Utente utente) {
		this.utente = utente;
	}

	public Byte[] getImage() {
		return image;
	}

	public void setImage(Byte[] image) {
		this.image = image;
	}

	public Annuncio getImmagineAnnuncio() {
		return immagineAnnuncio;
	}

	public void setImmagineAnnuncio(Annuncio immagineAnnuncio) {
		this.immagineAnnuncio = immagineAnnuncio;
	}
	
	
	
	
}
