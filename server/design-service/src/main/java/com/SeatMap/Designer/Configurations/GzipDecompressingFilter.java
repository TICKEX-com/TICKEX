package com.SeatMap.Designer.Configurations;

import jakarta.servlet.ReadListener;
import jakarta.servlet.ServletInputStream;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.core.annotation.Order;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;

import java.io.IOException;
import java.io.InputStream;
import java.util.zip.GZIPInputStream;

@Component
@Order(1)
public class GzipDecompressingFilter extends OncePerRequestFilter {
    @Override
    protected void doFilterInternal(jakarta.servlet.http.HttpServletRequest request, jakarta.servlet.http.HttpServletResponse response, jakarta.servlet.FilterChain filterChain) throws jakarta.servlet.ServletException, IOException {
        if (request.getHeader("Content-Encoding") != null && request.getHeader("Content-Encoding").contains("gzip")) {
            request = new GzipHttpServletRequestWrapper(request);
        }
        filterChain.doFilter(request, response);
    }

    private static class GzipHttpServletRequestWrapper extends jakarta.servlet.http.HttpServletRequestWrapper {

        public GzipHttpServletRequestWrapper(HttpServletRequest request) throws IOException {
            super(request);
        }

        @Override
        public ServletInputStream getInputStream() throws IOException {
            return new GzipServletInputStreamWrapper(new GZIPInputStream(super.getInputStream()));
        }
    }

    private static class GzipServletInputStreamWrapper extends ServletInputStream {

        private final InputStream delegate;

        public GzipServletInputStreamWrapper(InputStream delegate) {
            this.delegate = delegate;
        }

        @Override
        public int read() throws IOException {
            return delegate.read();
        }

        @Override
        public void close() throws IOException {
            delegate.close();
        }

        @Override
        public boolean isFinished() {
            return false;
        }

        @Override
        public boolean isReady() {
            return false;
        }

        @Override
        public void setReadListener(ReadListener readListener) {

        }
    }
}
