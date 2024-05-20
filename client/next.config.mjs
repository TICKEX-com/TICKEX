/** @type {import('next').NextConfig} */
const nextConfig = {
	webpack: (config) => {
		config.externals.push({
			sharp: "commonjs sharp",
			canvas: "commonjs canvas",
		});
		return config;
	},
	async rewrites() {
		return [
			{
				// Specific route for /details/categories/:id
				source: "/api/details/categories/:id*",
				destination: "http://localhost:8888/event-service/Events/:id*", // Redirect to a different backend
			},
			{
				source: "/api/:path*", // Adjust this according to your API route
				destination: "http://localhost:8888/:path*", // Adjust this according to your backend API endpoint
			},
			{
				source: "/:path*", // Catch-all route for dynamic paths
				destination: "/:path*", // Redirect to the same path on the client-side
			},
		];
	},
	images: {
		remotePatterns: [
			{
				protocol: "https",
				hostname: "firebasestorage.googleapis.com",
				port: "",
			},
		],
	},
};

export default nextConfig;
