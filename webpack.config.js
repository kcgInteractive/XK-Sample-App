const CssMinimizerPlugin = require("css-minimizer-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const TerserPlugin = require("terser-webpack-plugin");

const path = require("path");
module.exports = {
  entry: {
    site: "./client/src/App.js",
  },
  output: {
    filename: "[name].entry.js",
    path: path.resolve(__dirname, "wwwroot", "dist"),
    library: "$",
    libraryTarget: "umd",
  },
  target: "web",
  devtool: "source-map",
  mode: "development",
  module: {
    rules: [
      {
        test: /\.(scss|css)$/,
        use: [
          { loader: MiniCssExtractPlugin.loader },
          "css-loader",
          "sass-loader",
        ],
      },
      {
        test: /\.(eot|woff(2)?|ttf|otf|svg)$/i,
        type: "asset",
      },
    ],
  },

  optimization: {
    minimize: true,
    minimizer: [new CssMinimizerPlugin(), new TerserPlugin()],
  },
  plugins: [new MiniCssExtractPlugin({ filename: "[name].css" })],
};