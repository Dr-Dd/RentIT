package it.rentx.backend.service;

import java.util.ArrayList;
import java.util.List;

import javax.transaction.Transactional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import it.rentx.backend.models.Image;
import it.rentx.backend.models.frontendModel.ImageModel;
import it.rentx.backend.repository.ImageRepository;


@Service
public class ImageService {
	
	@Autowired
	 private ImageRepository imageRepo;
	
	@Transactional
	 public Image inserisci(Image i) {
		 return this.imageRepo.save(i);
	 }
	
	@Transactional
	public List<Image> getImagePerIdAnn(Long id){
		return this.imageRepo.findByAnnuncio_id(id);
	}
	
	@Transactional
	public Image trovaImmagine(Long id) {
		return this.imageRepo.findById(id).get();
	}
	
	@Transactional
	public boolean esiste(Long id) {
		return this.imageRepo.existsById(id);
	}
	
	public ImageModel parseToImage(Image i) {
		ImageModel im=new ImageModel(i.getData(), i.getId());
		return im;
	}
	
	public List<ImageModel> parseToImageList(List<Image> lista){
		List<ImageModel> l=new ArrayList<>();
		for(Image i: lista) {
			ImageModel tmp= this.parseToImage(i);
			l.add(tmp);
		}
		return l;
	}

}
