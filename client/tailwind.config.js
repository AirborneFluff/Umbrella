/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts,tsx,jsx}",
  ],
  theme: {
    fontFamily: {
      sans: ['Open Sans', 'Helvetica', 'Arial', 'sans-serif'],
      display: ['Poppins', 'Helvetica', 'Arial', 'sans-serif'],
    },
    extend: {
      colors: {
        'primary': '#5B7CFD',
        'primary-highlight': '#5270e4',
        'primary-deep': '#2e3e7f',
        'base-light': '#E7EEF7',
        'item-light': '#FFFFFF',
        'item-faded-light': '#F5F6F8'
      },
    },
  },
  plugins: [],
}

