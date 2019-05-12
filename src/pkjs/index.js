Pebble.addEventListener('ready', function() {
  require('pebblejs');
  var fable = require('./Program.js')
  var UI = require('pebblejs/ui');

  var card = new UI.Card({
    title: 'Hello World',
    body: fable.text,
    scrollable: true
  });

  card.show();
});
