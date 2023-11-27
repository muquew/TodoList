import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";;
import path from 'path'

// https://vitejs.dev/config/
export default defineConfig(async () => ({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, 'src'),
      '@@': path.resolve(__dirname, 'public'),
      '@tauri': path.resolve(__dirname, 'src-tauri')
    }
  },
  build: {
    rollupOptions: {
        input: {
            index: path.resolve(__dirname, 'index.html'),
            change: path.resolve(__dirname, 'src/page-htmls/change.html'),
            settings: path.resolve(__dirname, 'src/page-htmls/settings.html')
        }
    },
  },
  // Vite options tailored for Tauri development and only applied in `tauri dev` or `tauri build`
  //
  // 1. prevent vite from obscuring rust errors
  clearScreen: false,
  // 2. tauri expects a fixed port, fail if that port is not available
  server: {
    port: 1420,
    strictPort: false,
  }
}));
