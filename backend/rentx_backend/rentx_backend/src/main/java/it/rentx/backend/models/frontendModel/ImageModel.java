package it.rentx.backend.models.frontendModel;


public class ImageModel {
	
	private byte[] data;
	
	private long idImg;
	
	public ImageModel (byte[] data, long idImg) {
		this.data = data;
		this.idImg = idImg;
	}

	public byte[] getData() {
		return data;
	}

	public void setData(byte[] data) {
		this.data = data;
	}

	public long getIdImg() {
		return idImg;
	}

	public void setIdImg(long idImg) {
		this.idImg = idImg;
	}
	
	
}
