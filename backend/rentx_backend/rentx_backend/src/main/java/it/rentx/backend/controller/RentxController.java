package it.rentx.backend.controller;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class RentxController {
	
	@GetMapping("/task")
	public String helloWorld() {
		return "Hello, world";
	}

}
