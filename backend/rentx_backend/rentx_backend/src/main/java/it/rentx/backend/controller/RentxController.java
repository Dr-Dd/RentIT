package it.rentx.backend.controller;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/task")
public class RentxController {
	
	@GetMapping("/cane")
	public String helloWorld() {
		return "Hello, world";
	}

}
