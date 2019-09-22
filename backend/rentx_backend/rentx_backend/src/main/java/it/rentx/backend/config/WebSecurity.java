package it.rentx.backend.config;

import org.springframework.context.annotation.Bean;
import org.springframework.http.HttpMethod;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.web.cors.CorsConfiguration;
import org.springframework.web.cors.CorsConfigurationSource;
import org.springframework.web.cors.UrlBasedCorsConfigurationSource;

import it.rentx.backend.repository.UtenteRepository;
import it.rentx.backend.service.UserDetailsServiceImpl;

@EnableWebSecurity
public class WebSecurity extends WebSecurityConfigurerAdapter {
	private UserDetailsServiceImpl userDetailsService;
	private BCryptPasswordEncoder bCryptPasswordEncoder;
	private UtenteRepository utenteRepository;

	public WebSecurity(UserDetailsServiceImpl userDetailsService, BCryptPasswordEncoder bCryptPasswordEncoder, UtenteRepository utenteRepository) {
		this.userDetailsService = userDetailsService;
		this.bCryptPasswordEncoder = bCryptPasswordEncoder;
		this.utenteRepository = utenteRepository;
	}

	// Da modificare
	@Override
	protected void configure(HttpSecurity http) throws Exception {
		http.cors().and().csrf().disable().authorizeRequests()
				.antMatchers(HttpMethod.POST, SecurityConstants.SIGN_UP_URL).permitAll()
				.antMatchers("/utente/profile/**").permitAll()
				.antMatchers("/utente/image/**").permitAll()
				.antMatchers("/utente/**").authenticated()
				.antMatchers("/annuncio/newAnnuncio").authenticated()
				.antMatchers("/annuncio/elimina/**").authenticated()
				.antMatchers("/annuncio/modifica/**").authenticated()
				.antMatchers("/annuncio/annunci/**").permitAll()
				.antMatchers("/annuncio/addImage/**").authenticated()
				.antMatchers("/annuncio/prenota/**").authenticated()
				.antMatchers("/annuncio/libera/**").authenticated()
				.anyRequest().permitAll()
				.and().addFilter(new JWTAuthenticationFilter(authenticationManager(), utenteRepository))
				.addFilter(new JWTAuthorizationFilter(authenticationManager()))
				// this disables session creation on Spring Security
				.sessionManagement().sessionCreationPolicy(SessionCreationPolicy.STATELESS);
	}

	@Override
	public void configure(AuthenticationManagerBuilder auth) throws Exception {
		auth.userDetailsService(userDetailsService).passwordEncoder(bCryptPasswordEncoder);
	}

	@Bean
	CorsConfigurationSource corsConfigurationSource() {
		final UrlBasedCorsConfigurationSource source = new UrlBasedCorsConfigurationSource();
		source.registerCorsConfiguration("/**", new CorsConfiguration().applyPermitDefaultValues());
		return source;
	}
}