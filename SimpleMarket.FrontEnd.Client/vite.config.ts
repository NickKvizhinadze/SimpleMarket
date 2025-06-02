import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// Read from environment or default to 5173
const port = process.env.PORT ? parseInt(process.env.PORT, 10) : 5173;

console.log(`Vite dev server will use port: ${port}`);

export default defineConfig({
  plugins: [react()],
  server: {
    port,
    strictPort: true // Optional: fail if port is not available
  }
});


