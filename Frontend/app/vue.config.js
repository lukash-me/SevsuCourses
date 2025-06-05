// const { defineConfig } = require('@vue/cli-service')
// module.exports = defineConfig({
//   transpileDependencies: true,
//   devServer: {
//     proxy: 'http://localhost:5036',
//     protocol: "http",
//   },
// })

module.exports = {
  devServer: {
    hot: true,
    liveReload: true
  },
};