// const { defineConfig } = require('@vue/cli-service')
// module.exports = defineConfig({
//   transpileDependencies: true,
//   devServer: {
//     proxy: 'http://localhost:5036',
//     protocol: "http",
//   },
// })

const { VueLoaderPlugin } = require('vue-loader');

module.exports = {
  devServer: {
    hot: false,
    liveReload: false
  },
  module: {
    rules: [
      {
        test: /\.vue$/,
        loader: 'vue-loader'
      },
    ]
  },
  plugins: [
    new VueLoaderPlugin()
  ],
};