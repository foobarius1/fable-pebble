// "polyfill" for fable libs to work
window.Symbol = { iterator: 'Symbol.iterator' }

Pebble.addEventListener('ready', function() {
  require('pebblejs')

  var app = require('./App.js')
  app.run()
});
