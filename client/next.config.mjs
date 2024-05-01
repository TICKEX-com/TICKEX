/** @type {import('next').NextConfig} */
const nextConfig = {
    async rewrites() {
        return [
          {
              source: '/:path*',
              destination: 'http://localhost:8888/:path*',
          },
        ];
      },
      images: {
        remotePatterns: [
          {
            protocol: 'https',
            hostname: 'firebasestorage.googleapis.com',
            port: '',
          },
        ],
    
    }
};


export default nextConfig;
