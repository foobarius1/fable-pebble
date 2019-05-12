module.exports = {
  entry: 'src/fs/App.fsproj',
  outDir: 'src/pkjs',
  babel: {
    presets: [["@babel/preset-env", { modules: "commonjs" }]],
  },
}
