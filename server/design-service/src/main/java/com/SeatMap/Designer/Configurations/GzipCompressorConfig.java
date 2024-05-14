package com.SeatMap.Designer.Configurations;

import org.springframework.boot.web.servlet.FilterRegistrationBean;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class GzipCompressorConfig {
    @Bean
    public FilterRegistrationBean<GzipDecompressingFilter> gzipDecompressingFilterRegistration() {
        FilterRegistrationBean<GzipDecompressingFilter> registration = new FilterRegistrationBean<>();
        registration.setFilter(new GzipDecompressingFilter());
        registration.addUrlPatterns("/*");
        return registration;
    }
}
