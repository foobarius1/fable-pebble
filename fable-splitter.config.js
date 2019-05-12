module.exports = {
  entry: 'src/App.fsproj',
  outDir: 'build',
  babel: {
    presets: [["@babel/preset-env", { modules: "commonjs" }]],
  },
}
