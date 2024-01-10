/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    fontFamily: {
      sans: ['Open Sans', 'Helvetica', 'Arial', 'sans-serif'],
      display: ['Poppins', 'Helvetica', 'Arial', 'sans-serif'],
    },
    colors: {
      'primary': '#5B7CFD',
      'base-light': '#E7EEF7',
      'item-light': '#FFFFFF',
      'item-faded-light': '#F5F6F8',
      // Configure your color palette here
    },
    extend: {},
  },
  plugins: [],
}

