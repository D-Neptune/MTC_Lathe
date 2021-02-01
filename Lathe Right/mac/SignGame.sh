#!/bin/bash
echo "Copying provisioning profile..."
cp ./embedded.provisionprofile ./Lathe Right.app/Contents/
cp ./info.plist ./Lathe Right.app/Contents/
echo "Starting Signing..."

codesign -f --deep -s "Unity Developer Application: David Neptune & Philipe Rosier" --entitlements "./Lathe Right.entitlements" ./Lathe Right.app/Contents/Frameworks/MonoEmbedRuntime/osx/libmono.0.dylib
codesign -f --deep -s "Unity Developer Application: David Neptune & Philipe Rosier" --entitlements "./Lathe Right.entitlements" ./Lathe Right.app/Contents/Frameworks/MonoEmbedRuntime/osx/libMonoPosixHelper.dylib
codesign -f --deep -s "Unity Developer Application: David Neptune & Philipe Rosier" --entitlements Lathe Right.entitlements "./Lathe Right.app/"

echo "Done Signing..."

echo "Packaging game..."
productbuild --component "./Lathe Right.app" "/Applications" --sign "Unity Developer Application: David Neptune & Philipe Rosier" YOUR_GAME_NAME.pkg
