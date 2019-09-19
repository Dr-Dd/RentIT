package it.rentx.backend.models.frontendModel;

import java.util.Date;

public class AnnuncioModel {

	private long id;
	
	private long AffittuarioId;

	private byte[] anteprimaImg;

	private String nomeOggetto;

	private String descrizione;

	private float prezzo;

	private String posizione;

	private Date data;

	public AnnuncioModel(long id, long idAffituario, byte[] anteprimaImg, String nomeOggetto, String descrizione,
			float prezzo, String posizione, Date data) {
		super();
		this.id = id;
		this.anteprimaImg = anteprimaImg;
		this.nomeOggetto = nomeOggetto;
		this.descrizione = descrizione;
		this.prezzo = prezzo;
		this.posizione = posizione;
		this.data = data;
	}

	public long getId() {
		return id;
	}

	public void setId(long id) {
		this.id = id;
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

	public long getAffittuarioId() {
		return AffittuarioId;
	}

	public void setAffittuarioId(long affittuarioId) {
		AffittuarioId = affittuarioId;
	}
	
	
	
}
